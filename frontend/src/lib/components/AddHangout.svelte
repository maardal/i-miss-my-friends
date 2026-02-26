<script lang="ts">
	import { getHangoutState } from '$lib/stores/HangoutStore.svelte';
	import { getLovedOneState } from '$lib/stores/LovedOneStore.svelte';
	import type { LovedOne } from '$lib/types/types';
	import { createHangout, fetchLovedOne } from '../../api/immfapi';
	const { loved } = $props<{ loved: LovedOne }>();

	const HangoutStore = getHangoutState();
	const LovedOneStore = getLovedOneState();

	async function addHangout(lovedId: string) {
		const hangout = await createHangout(lovedId);
		hangout != undefined
			? HangoutStore.addHangout(hangout)
			: console.log('temp - hangout not created');
	}

	async function refetchLovedOne(lovedId: string) {
		const lovedOne = await fetchLovedOne(lovedId);
		lovedOne != undefined
			? LovedOneStore.updateLovedOne(lovedOne)
			: console.log('temp - lovedone not updated and fetched');
	}

	async function handleAddHangout(lovedId: string) {
		await addHangout(lovedId);
		await refetchLovedOne(lovedId);
	}
</script>

<button
	onclick={() => {
		handleAddHangout(loved.id);
	}}
	>Add hangout
</button>
