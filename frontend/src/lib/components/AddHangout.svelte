<script lang="ts">
	import { getHangoutState, type Hangout } from '$lib/stores/HangoutStore.svelte';
	import type { LovedOne } from '$lib/types/types';
	import { createHangout } from '../../api/immfapi';
	const { loved } = $props<{ loved: LovedOne }>();

	const HangoutStore = getHangoutState();

	async function addHangout(lovedId: string) {
		const hangout = await createHangout(lovedId);
		hangout != undefined
			? HangoutStore.addHangout(hangout)
			: console.log('temp - hangout not created');
	}
</script>

<button onclick={() => addHangout(loved.id)}>Add hangout </button>
