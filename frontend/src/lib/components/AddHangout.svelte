<script lang="ts">
	import { getHangoutState } from '$lib/stores/HangoutStore.svelte';
	import { getLovedOneState } from '$lib/stores/LovedOneStore.svelte';
	import { type Hangout } from '$lib/types/types';
	import type { LovedOne } from '$lib/types/types';
	import { createHangout } from '../../api/immfapi';
	const { loved } = $props<{ loved: LovedOne }>();

	const HangoutStore = getHangoutState();
	const LovedOneState = getLovedOneState();

	async function addHangout(lovedId: string) {
		const hangout = await createHangout(lovedId);
		hangout != undefined
			? HangoutStore.addHangout(hangout)
			: console.log('temp - hangout not created');
	}

	async function refetchLovedOne(lovedId: string) {}
</script>

<button
	onclick={() => {
		addHangout(loved.id);

		console.log(HangoutStore.hangouts.length);
	}}
	>Add hangout
</button>
