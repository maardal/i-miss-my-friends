<script lang="ts">
	import AddHangout from './AddHangout.svelte';
	import type { LovedOne } from '$lib/types/types';
	const { loved } = $props<{ loved: LovedOne }>();
</script>

<section>
	<p>{loved.relationship}: {loved.name} + {loved.lastHangout}</p>
	<AddHangout {loved}></AddHangout>
</section>
